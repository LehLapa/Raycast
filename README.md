# Raycast
### Descrição
Esse projeto foi desenvolvido usando a Game Engine *Unity*, afim de explorar as funcionalidades do *RayCast*. O jogador tem como objetivo eliminar as esferas que são geradas automaticamente. 

### Criadores
- Aluna: Fernanda Viana Marcelino
- Aluna: Letícia da Lapa Silva

**Cursando:** ETEC Professor Basilides de Godoy - Ensino Médio com Habilitação Profissional Técnico em Programação de Jogos Digitais. 3ºA/2024
### Requisitos 
Para a execução do projeto é necessário a instalação do *Unity* na versão __*2023.2.11f1.*__
Acesso para o Drive: https://drive.google.com/drive/u/0/folders/18h9f5nC51sQCjMsgSg5VzN2IYAmX-fRx
## Desenvolvimento Visual
### Assets Utilizados
Para o desenvolvimento da cena, foi necessário a instalação dos seguintes assets de uso livre: 
+ Casas
  
![AssetCasa](https://github.com/LehLapa/Raycast/assets/128320607/3378951d-e8bb-46ef-a14d-05901581f780)

+ Floresta
  
![AssetFloresta](https://github.com/LehLapa/Raycast/assets/128320607/2de34dca-01a0-4794-8a29-bbe487e34cb6)

+ SkyBox
  
![SkyBox](https://github.com/LehLapa/Raycast/assets/128320607/d88a6787-acfd-4f69-8e9e-1a3e2abc3122)

### Processo de Criação da Cena
Para criar o design da cena seguimos os seguintes passos: 
1. Adicionamos o chão e os assets da casa na cena
   
![passo1](https://github.com/LehLapa/Raycast/assets/128320607/f0053d84-0d23-418e-960c-7f10ba66d342)

2. Adicionamos as árvores para complementar o cenário
   
![passo2](https://github.com/LehLapa/Raycast/assets/128320607/a56edb0a-bd50-4796-a1af-5b78663dd770)

3. Adicionamos o SkyBox no céu

![passo3](https://github.com/LehLapa/Raycast/assets/128320607/9ba9b73d-9ecb-4fbb-a6fa-625f571ec2e0)

4. Adicionamos as luzes e o GameObject das esferas

![passo4](https://github.com/LehLapa/Raycast/assets/128320607/53d7fd68-368a-4a63-8beb-053d8f28950f)

### Imagens de Gameplay
A seguir, as imagens do jogo em execução, podendo observar que as esferas continuam sendo geradas em diversas posições, mesmo quando as anteriores são detruidas. 

![game1](https://github.com/LehLapa/Raycast/assets/128320607/e5b206fc-47c1-4249-852d-c199890fea5c)

![game2](https://github.com/LehLapa/Raycast/assets/128320607/83bee11a-d38f-4f4e-b64f-2515505c11ee)

## Scripts
Para a criação do código seguimos os seguintes passos: 
1. Iniciando o código, declaramos os atributos e métodos que iremos utilizar, como: `Ray`, `RaycastHit`, `Vector3`, `Color` e a Câmera `(public Camera _ camera;)`
No Start, iniciamos a coroutine e nosso método para gerar as esferas.

```ruby
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

````
2. `Void Update` é aplicado o lançamento do Ray, determinamos que raio será lançado pelo clique do botão direito do mouse e seguindo as cordenadas da posição do mouse na tela. 
`Void Lancar`, toda a parte de Debug está sendo adicionada. Identificando se o alvo foi atingido e destruído ou não, esclarecendo posição e direção. Caso não tenha sido atingido, será retornado o valor "Target Missed".

```ruby
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
```
3. O método `'SphereIndicator'` irá criar um indicador visual na forma de uma esfera em uma determinada posição. 
- **Vector3 pos**: É a posição onde o indicador será criado.
- **int tipo**: É o tipo de indicador a ser criado. Pode ser 1, 2 ou 3.
- **switch (tipo)**: Seleciona o tipo de indicador com base no valor de tipo.
- **case 1**: Se tipo for 1, cria um objeto esférico (chamado "cherry").
- **case 2**: Se tipo for 2, instancia um prefab de bomba.
- **case 3**: Se tipo for 3, cria um objeto esférico azul.
- **yield return new WaitForSeconds(1)**: Aguarda por 1 segundo antes de destruir o objeto criado.
- **Destroy(gameObj)**: Destrói o objeto criado após 1 segundo.
O Void `'OnDrawGizmosSelected'`: irá ser chamado para definir o raio na cena.
O método `'CriaObject'`: Irá criar nosso alvo, a esfera, e devidir com um material específico e uma determinada posição.
Método `'InstancisPrefab'`: Irá instanciar o prefab, targer sphere, em uma determinada posição.

```ruby
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
```
4. O código apresentado `'GerarTarget'` é uma coroutine que executa um loop infinito `('while (true)')` para gerar os alvos em posições aleatórias em um intervalo de tempo.
Logo em seguida é especificado as posições X, Y e Z que serão as coordenadas aleatórias para gerar as esferas dentro do loop.
Instanciamos os alvos e determinados o tempo, fazendo a coroutine aguardar por 1 segundo até gerar a próxima esfera em coordenadas aleatórias que definimos anteriormente

```ruby
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
```
