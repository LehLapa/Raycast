using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycode : MonoBehaviour
{

    Ray ray;
    RaycastHit hitData;
    Vector3 point;
    Color color;

    public Camera _camera;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("inicio!");
        StartCoroutine(GerarTarget());
    }

    // Update is called once per frame
    void Update()
    {


        if (UnityEngine.Input.GetKey(KeyCode.Mouse0))
        {

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            color = Color.green;
            Lancar(ray, color, 1);

        }
    }
    private void Lancar(Ray ray, Color color, int tipo)
    {
        Debug.Log("Origem: " + ray.origin);
        Debug.Log("Direções: " + ray.direction);

        if (Physics.Raycast(ray, out hitData))
        {
            Vector3 hitPosition = hitData.point;
            Debug.Log(" hitPosition:" + hitPosition);


            float hitDistance = hitData.distance;
            Debug.Log("Distancia: " + hitDistance);
            string tag = hitData.collider.tag;
            Debug.Log("Tag:" + tag);
            GameObject hitObject = hitData.transform.gameObject;
            Debug.DrawRay(ray.origin, hitPosition * hitDistance, color);
            StartCoroutine(SphereIndicator(hitPosition, tipo));

            if (tag == "target")
                Destroy(hitObject);

            else 
            { 
                Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red);
            }
        }


    }
    private IEnumerator SphereIndicator(Vector3 pos, int tipo)
    {
        GameObject gameObj = null;
        switch (tipo)
        {
            case 1:
                gameObj = CriaObject(pos, "cherry");
                break;
            case 2:
                gameObj = InstanciaPrefab(pos);
                break;
            case 3:
                gameObj = CriaObject(pos, "blue");
                break;
        }

        yield return new WaitForSeconds(1);
        Destroy(gameObj);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(ray);
    }
    GameObject CriaObject(Vector3 pos, string material)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = pos;
        sphere.transform.localScale = new Vector3(1, 1, 1);
        string src = string.Concat("material/", material);
        Material bombMaterial = Resources.Load(src, typeof(Material)) as Material;

        sphere.GetComponent<Renderer>().material = bombMaterial;
        return sphere;

    }
    GameObject InstanciaPrefab(Vector3 pos)
    {
        GameObject prefab = Resources.Load("prefab/bomb", typeof(GameObject)) as GameObject;
        return Instantiate(prefab, pos, Quaternion.identity);
    }

    private IEnumerator GerarTarget()
    {
        while (true)
        {
            float x = Random.Range(-23.0f, -5.0f);
            float y = Random.Range(5.0f, -2.0f);
            float z = Random.Range(-2.0f, 3.0f);
            Vector3 position = new Vector3(x, y, z);
            Instantiate(Resources.Load("prefabs/target", typeof(GameObject)) as GameObject, position, Quaternion.identity);
            yield return new WaitForSeconds(1);
        }
    }
}

