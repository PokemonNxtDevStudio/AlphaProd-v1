
using UnityEngine;
using System.Collections;
using System;

namespace NXT.PhysX
{
public class ProjectileController : MonoBehaviour
{

    public GameObject projectile;
    public bool isSemiAutomatic;
    public float verticalSpread = 0;
    public float horizontalSpread = 0;
    public int projectileCount = 1;
    public float projectileLife = 5;
    public Vector3 projectileAcceleration;
    public Vector3 projectileGlobalAcceleration;
    public float reloadTime = 1;

    public float projectileSpeed = 5;
    public float projectileSpeedDelta = 0;
    public float accelerationScale;
    public float displayScale = 1;

    public Vector3 displayOffset = -Vector3.one * 10 + Vector3.right * 20;


    public bool showDisplayHandles = false;
    public bool gizmosFlag = true;


    public float projectileMass = 1f;

    public AudioSource audioSource;

    void Awake()
    {
        //base.Awake();
        //timeBeforeDeath = projectileLife;
    }

    void Start()
    {
 

    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0)) //add this to input controller etc
            Spawnprojectile(Type.GetType("Projectile"));
        // or Spawnprojectile();
    }



  
    public virtual void Throw()
    {
      

    }

    public virtual void Spawnprojectile(Type projectileType= null)
    {
        for (int i = 0; i < projectileCount; i++)
        {
            GameObject newprojectile = (GameObject)Instantiate(projectile, transform.position, transform.rotation);
            newprojectile.name = "Defaultprojectile";
            Rigidbody rb =  newprojectile.AddComponent<Rigidbody>();

         
            rb.mass = projectileMass;
            rb.drag = 0;
            rb.useGravity = false;

            //Example Runtime Type add
            if (projectileType != null)
            {
                Projectile p;
                if ((p = (Projectile)newprojectile.GetComponent(projectileType)) == null)
                    p = (Projectile)newprojectile.AddComponent(projectileType);
                p.timeBeforeDeath = projectileLife;
            }
            rb.velocity =
     Camera.main.transform.forward * (projectileSpeed + projectileSpeedDelta * (UnityEngine.Random.value - 0.5f) * 2)
                    + Camera.main.transform.up * (verticalSpread * (UnityEngine.Random.value - 0.5f) * 2)
                    + Camera.main.transform.right * (horizontalSpread * (UnityEngine.Random.value - 0.5f) * 2);

           ConstantForce cf = newprojectile.AddComponent<ConstantForce>();
           cf.force = (projectileAcceleration.x * Camera.main.transform.forward
                    + projectileAcceleration.y * Camera.main.transform.up
                    + projectileAcceleration.z * Camera.main.transform.right) * accelerationScale;
           cf.force += (projectileGlobalAcceleration) * accelerationScale;
           cf.force *= 50f;

            //newprojectile.AddComponent<APEprojectile> ().life = projectileLife;
        }
    }

    public Vector3 DisplayPosition(bool swap = false)
    {
        Vector3 result = DisplayOffset(swap) + transform.position;
        return result;
    }

    public Vector3 DisplayOffset(bool swap = false)
    {
        Vector3 result = displayOffset.z * transform.forward * transform.lossyScale.z
                + displayOffset.y * transform.up * transform.lossyScale.y
                + displayOffset.x * transform.right * transform.lossyScale.x;
        if (!swap)
        {
            return result;
        }
        else
        {
            return new Vector3(result.z, result.y, result.x);
        }
    }

    void OnDrawGizmos()
    {
        if (gizmosFlag)
        {
            DrawAPEGizmos();
        }
    }
    void OnDrawGizmosSelected()
    {
        if (!gizmosFlag)
        {
            DrawAPEGizmos();
        }
    }

    void DrawAPEGizmos()
    {
        if (this.enabled)
        {
            //Shoot cone display
            Mesh hull = new Mesh();
            hull.Clear();
            Vector3[] vertexMatrix = new Vector3[8];
            Vector3[] dots1, dots2;
            dots1 = DrawShot(transform.forward * (projectileSpeed + projectileSpeedDelta), projectileLife, Color.gray);
            dots2 = DrawShot(transform.forward * (projectileSpeed - projectileSpeedDelta), projectileLife, Color.gray);
            System.Array.Copy(dots1, 0, vertexMatrix, 0, dots1.Length);
            System.Array.Copy(dots2, 0, vertexMatrix, dots1.Length, dots2.Length);
            hull.vertices = vertexMatrix;
            Vector3[] except = null;
            DrawMesh(hull, except);
            Gizmos.DrawWireSphere(DrawTrajectory(transform.position, transform.forward * (projectileSpeed), projectileLife, Color.yellow), 0.3f);



         
          
            
                Gizmos.color = Color.white;
            
            Gizmos.DrawLine(transform.position + displayScale * transform.up, transform.position - displayScale * transform.up);
            Gizmos.DrawLine(transform.position + displayScale * transform.right, transform.position - displayScale * transform.right);
            Gizmos.DrawLine(transform.position + displayScale * transform.forward * 1.5f, transform.position - displayScale * transform.forward);
            Gizmos.DrawLine(transform.position + displayScale * transform.forward * 1.5f, transform.position - transform.up * displayScale / 2);
            Gizmos.DrawLine(transform.position + displayScale * transform.forward * 1.5f, transform.position + transform.up * displayScale / 2);
            Gizmos.DrawLine(transform.position + displayScale * transform.forward * 1.5f, transform.position - transform.right * displayScale / 2);
            Gizmos.DrawLine(transform.position + displayScale * transform.forward * 1.5f, transform.position + transform.right * displayScale / 2);
            Gizmos.DrawWireSphere(transform.position, 0.475f * displayScale);

            //projectiles per shot display
            Gizmos.color = Color.cyan;
            for (float i = 0; i < projectileCount; i++)
            {
                float size = 0.5f;
                if (i % 5 == 0)
                {
                    size = 1.0f;
                }
                if (i % 10 == 0)
                {
                    size = 2.0f;
                }
                Gizmos.DrawLine(transform.position + transform.up * i / projectileCount * displayScale * 2,
                transform.position + transform.forward * size * displayScale + transform.up * i / projectileCount * displayScale * 2);
            }

            DestroyImmediate(hull);
        }
    }

    public int IsOverCannon()
    {
        if (displayOffset.y > 0)
        {
            return +1;
        }
        if (displayOffset.y < 0)
        {
            return -1;
        }
        return 0;
    }

    Vector3 DrawTrajectory(Vector3 pos, Vector3 vel, float time, Color color, bool draw = true)
    {
        Gizmos.color = color;
        Vector3 nextPos;
        for (float i = 0; i < time; i += Time.fixedDeltaTime)
        {
            nextPos = pos + Time.fixedDeltaTime * vel;
            if (draw)
            {
                Gizmos.DrawLine(pos, nextPos);
            }
            pos = nextPos;
            Vector3 accel = (projectileAcceleration.x * transform.forward + projectileAcceleration.y * transform.up + projectileAcceleration.z * transform.right + projectileGlobalAcceleration);
            vel += (accel) * accelerationScale;
        }
        return pos;
    }

    public Vector3 ShotEnd(Vector3 initialSpeed, float time, Color color)
    {
        return DrawTrajectory(transform.position, initialSpeed, time, color, false);
    }

    Vector3[] DrawShot(Vector3 initialSpeed, float time, Color color, bool draw = true)
    {
        Vector3[] res = new Vector3[4];
        res[0] = DrawTrajectory(transform.position, initialSpeed - transform.up * verticalSpread - transform.right * horizontalSpread, time, color, draw);
        res[1] = DrawTrajectory(transform.position, initialSpeed + transform.up * verticalSpread - transform.right * horizontalSpread, time, color, draw);
        res[2] = DrawTrajectory(transform.position, initialSpeed + transform.up * verticalSpread + transform.right * horizontalSpread, time, color, draw);
        res[3] = DrawTrajectory(transform.position, initialSpeed - transform.up * verticalSpread + transform.right * horizontalSpread, time, color, draw);
        Gizmos.color = Color.white;
        return res;
    }

    void ExceptionLine(Vector3 source, Vector3 destination)
    {
        bool skip = true;

        Vector3 s = transform.worldToLocalMatrix.MultiplyVector(source);
        Vector3 d = transform.worldToLocalMatrix.MultiplyVector(destination);

        if (RoughlyEqual(s.x, d.x) || RoughlyEqual(s.y, d.y) || RoughlyEqual(s.z, d.z))
        {
            skip = false;
        }
        if (RoughlyEqual(s.x, d.x) ^ RoughlyEqual(s.y, d.y) ^ RoughlyEqual(s.z, d.z))
        {
            skip = true;
        }

        if (!skip)
        {
            Gizmos.DrawLine(source, destination);
        }
    }

    void DrawMesh(Mesh mesh, Vector3[] exception)
    {
        foreach (Vector3 vert in mesh.vertices)
        {
            Gizmos.color = Color.white;
            foreach (Vector3 check in mesh.vertices)
            {
                ExceptionLine(vert, check);
            }
        }
    }

    static bool RoughlyEqual(float a, float b)
    {
        float treshold = 0.01f;
        return (Mathf.Abs(a - b) < treshold);
    }
}
}