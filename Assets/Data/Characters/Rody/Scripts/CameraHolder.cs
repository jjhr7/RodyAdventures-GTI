using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CameraHolder : MonoBehaviour
{
    InputHandler inputHandler;
    PlayerManager playerManager;
    public Transform targetTransform;
    public Transform cameraTransform; //the main camera
    public Transform cameraPivotTransform; //rotation of the camera
    private Transform myTransform;
    private Vector3 cameraTransformPosition;
    private LayerMask ignoreLayers;
    public LayerMask enviromentLayer;
    private Vector3 cameraFollowVelocity = Vector3.zero;

    public static CameraHolder singleton;

    public float lookSpeed = 0.1f;
    public float followSpeed = 0.1f;
    public float pivotSpeed = 0.03f;

    private float targetPosition;
    private float defaultPosition;
    private float lookAngle;
    private float pivotAngle;
    public float minimumPivot = -35;
    public float maximumPivot = 35;

    public float cameraSphereRadious = 0.2f;
    public float cameraCollisionOffset = 0.2f;
    public float minimumCollisionOffset = 0.2f;
    //modo enfoque
    public float lockedPivotPosition = 2.25f; //pivot position del modo enfoque
    public float unlockedPivotPosition = 1.65f;

    //var modo enfoque
    public CharacterManager currentLockOnTarget; //var donde guardo lo que estoy enfocando

    public float maximumLockOnDistance = 30; // distancia maxima de enfoque
    public CharacterManager nearestLookOnTarget;
    public CharacterManager leftLockTarget;
    public CharacterManager rightLockTarget;
    List<CharacterManager> availableTargets = new List<CharacterManager>(); // objetos en los que nos podemos enfocar

    private void Awake() //se llama al awake al cargar la instancia del script
    {
        singleton = this;
        myTransform = transform;
        defaultPosition = cameraTransform.localPosition.z;
        ignoreLayers = ~(1 << 8 | 1 << 9 | 1 << 10);
        //aca se pone los getComponent/ en este caso find object para llamar las otras clases
        targetTransform = FindObjectOfType<PlayerManager>().transform; //encuentra al personaje si se bugea la camara
        inputHandler = FindObjectOfType<InputHandler>();
        playerManager = FindObjectOfType<PlayerManager>();
    }

    private void Start()
    {
        enviromentLayer = LayerMask.NameToLayer("Environment"); //give the name of the layer via text
    }

    public void FollowTarget(float delta)
    {       //interpolacion entre la posicion del objeto y la posicion de la camara
            //Vector3 targetPosition = Vector3.Lerp(myTransform.position, targetTransform.position, delta / followSpeed);
        Vector3 targetPosition = Vector3.SmoothDamp(myTransform.position, targetTransform.position, ref cameraFollowVelocity, delta / followSpeed);
        myTransform.position = targetPosition; //con esto la camara seguira al objeto

        HandleCameraCollisions(delta);
    }

    public void HandleCameraRotation(float delta, float mouseXInput, float mouseYInput)
    {
        if (inputHandler.lockOnFlag == false && currentLockOnTarget == null)
        {
            lookAngle += (mouseXInput * lookSpeed) / delta;
            pivotAngle -= (mouseYInput * pivotSpeed) / delta;
            pivotAngle = Mathf.Clamp(pivotAngle, minimumPivot, maximumPivot); //media del maximo y minimo

            Vector3 rotation = Vector3.zero;
            rotation.y = lookAngle;
            Quaternion targetRotation = Quaternion.Euler(rotation);
            myTransform.rotation = targetRotation;

            rotation = Vector3.zero;
            rotation.x = pivotAngle;

            targetRotation = Quaternion.Euler(rotation);
            cameraPivotTransform.localRotation = targetRotation;
        }
        else
        {
            //forzamos a la camara a rotar hacia la direccion del target
            float velocity = 0;
            Vector3 dir = currentLockOnTarget.transform.position - transform.position;
            dir.Normalize();
            dir.y = 0;

            Quaternion targetRotation = Quaternion.LookRotation(dir);
            transform.rotation = targetRotation;
            dir = currentLockOnTarget.transform.position - cameraPivotTransform.position;
            dir.Normalize();
            targetRotation = Quaternion.LookRotation(dir);
            Vector3 eulerAngle = targetRotation.eulerAngles;
            eulerAngle.y = 0;
            cameraPivotTransform.localEulerAngles = eulerAngle;
        }

    }

    private void HandleCameraCollisions(float delta)
    {
        targetPosition = defaultPosition;
        RaycastHit hit; //cada vez que la camara se choca con un collider es true
        Vector3 direction = cameraTransform.position - cameraPivotTransform.position;
        direction.Normalize();

        if (Physics.SphereCast(cameraPivotTransform.position, cameraSphereRadious, direction, out hit, Mathf.Abs(targetPosition), ignoreLayers))
        {
            float dis = Vector3.Distance(cameraPivotTransform.position, hit.point);
            targetPosition = -(dis - cameraCollisionOffset);
        }

        if (Mathf.Abs(targetPosition) < minimumCollisionOffset)
        {
            targetPosition = -minimumCollisionOffset;
        }
        cameraTransformPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPosition, delta / 0.2f);
        cameraTransform.localPosition = cameraTransformPosition;
    }

    public void HandleLockOn() //enfoque enemigos
    {
        float shortestDistance = Mathf.Infinity; //manejador de distancias entre enemigos cerca nuestro
        float shortestDistanceOfLeftTarget = -Mathf.Infinity;
        float shortestDistanceOfRightTarget = Mathf.Infinity;

        //targetTransform.position -> posicion de nuestro jugador
        Collider[] colliders = Physics.OverlapSphere(targetTransform.position, 26); //crea una esfera invisible alrededor del jugador

        for (int i = 0; i < colliders.Length; i++) //for el cual guardamos todos los collideres que detecta la esfera
        {
            CharacterManager character = colliders[i].GetComponent<CharacterManager>();

            if (character != null)
            {
                Vector3 lockTargetDirection = character.transform.position - targetTransform.position;// enfoque dirrecion del player
                float distanceFromTarget = Vector3.Distance(targetTransform.position, character.transform.position); //distancia
                                                                                                                     //si el target esta fuera de pantalla el enfoque no funciona, siempre tiene que estar a la vista del player
                float viewableAngle = Vector3.Angle(lockTargetDirection, cameraTransform.forward); //angulo en el que se permite el enfoque
                RaycastHit hit;

                //este if lo que hace es que no podamos enfocarnos a nosotros mismos , comparando las posicioness
                if (character.transform.root != targetTransform.transform.root
                    && viewableAngle > -50 && viewableAngle < 50 && distanceFromTarget <= maximumLockOnDistance)
                {
                    if(Physics.Linecast(playerManager.lookOnTransform.position,character.lookOnTransform.position, out hit) ) //si hay pared no hacer enfoque
                    {
                        Debug.DrawLine(playerManager.lookOnTransform.position, character.lookOnTransform.position);

                        if(hit.transform.gameObject.layer == enviromentLayer)
                        {
                            //no puedes enfocar nada dentras de una pared por ejemplo
                        }
                        else
                        {
                            availableTargets.Add(character); //anyadimos el objeto que nos podemos fijar en la lista
                        }
                    }
                   
                }
            }
        }

        for (int k = 0; k < availableTargets.Count; k++) // for que recorre los objetos enfocables
        {
            //distancia del player al target enfocables
            float distanceFromTarget = Vector3.Distance(targetTransform.position, availableTargets[k].transform.position);
            if (distanceFromTarget < shortestDistance) //si la distancia del P al enfocalbe es menor que la minima distancia
            {
                shortestDistance = distanceFromTarget; //la minima distancia se sustituye
                nearestLookOnTarget = availableTargets[k]; //objeto mas cercano a nosotros
            }

            if (inputHandler.lockOnFlag)
            {
                //Vector3 relativeEnemyPosition = currentLockOnTarget.transform.InverseTransformPoint(availableTargets[k].transform.position);
                //var distanceFromLeftTarget = currentLockOnTarget.transform.position.x - availableTargets[k].transform.position.x;
                //var distanceFromRightTarget = currentLockOnTarget.transform.position.x + availableTargets[k].transform.position.x;
                Vector3 relativeEnemyPosition = inputHandler.transform.InverseTransformPoint(availableTargets[k].transform.position);
                var distanceFromLeftTarget = relativeEnemyPosition.x;
                var distanceFromRightTarget = relativeEnemyPosition.x;

                if (relativeEnemyPosition.x <= 0.00 && distanceFromLeftTarget > shortestDistanceOfLeftTarget 
                    && availableTargets[k] != currentLockOnTarget) //left target lockOn
                {
                    shortestDistanceOfLeftTarget = distanceFromLeftTarget;
                    leftLockTarget = availableTargets[k];
                }
                else if (relativeEnemyPosition.x >= 0.00 && distanceFromRightTarget < shortestDistanceOfRightTarget
                    && availableTargets[k] != currentLockOnTarget) //right target lockOn
                {
                    shortestDistanceOfRightTarget = distanceFromRightTarget;
                    rightLockTarget = availableTargets[k];
                }
            }
        }
    }

    public void ClearLockOnTarget()
    {
        availableTargets.Clear();
        nearestLookOnTarget = null;
        currentLockOnTarget = null;
    }

    public void SetCameraHeight()
    {
        Vector3 velocity = Vector3.zero;
        Vector3 newLockedPosition = new Vector3(0, lockedPivotPosition);
        Vector3 newUnlockedPosition = new Vector3(0, unlockedPivotPosition);

        if (currentLockOnTarget != null)
        {
            cameraPivotTransform.transform.localPosition = Vector3.SmoothDamp(cameraPivotTransform.transform.localPosition, newLockedPosition, ref velocity, Time.deltaTime);
        }
        else
        {
            cameraPivotTransform.transform.localPosition = Vector3.SmoothDamp(cameraPivotTransform.transform.localPosition, newUnlockedPosition, ref velocity, Time.deltaTime);
        }
    }
}


