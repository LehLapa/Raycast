# Raycast
## Descrição
Esse projeto foi desenvolvido usando o Unity como game engine, afim de explorar as funcionalidades do RayCast. O jogador tem como objetivo eliminar as esferas que são geradas automaticamente. 
## Criadores
+ Fernanda Viana e Letícia Lapa
# Requisitos 
Para a execução do projeto é necessário a instalação do Unity na versão 2023.2.11f1.
# Desenvolvimento Visual
## Assets Utilizados
Para o desenvolvimento da cena, foi necessários a instalação dos seguintes assets de uso livre: 
+ Casa
![AssetCasa](https://github.com/LehLapa/Raycast/assets/128320607/3378951d-e8bb-46ef-a14d-05901581f780)
+ Floresta
![AssetFloresta](https://github.com/LehLapa/Raycast/assets/128320607/2de34dca-01a0-4794-8a29-bbe487e34cb6)
+ SkyBox
![SkyBox](https://github.com/LehLapa/Raycast/assets/128320607/d88a6787-acfd-4f69-8e9e-1a3e2abc3122)

## Processo de Criação da Cena
Para criar o design da cena seguimos os seguintes passos: 
1. Adicionamos o chão e os assets da casa na cena
![passo1](https://github.com/LehLapa/Raycast/assets/128320607/f0053d84-0d23-418e-960c-7f10ba66d342)
2. Adionamos as árvores para complementar o cenário
![passo2](https://github.com/LehLapa/Raycast/assets/128320607/a56edb0a-bd50-4796-a1af-5b78663dd770)
3. Adicionamos o SkyBox no céu
![passo3](https://github.com/LehLapa/Raycast/assets/128320607/9ba9b73d-9ecb-4fbb-a6fa-625f571ec2e0)
4. Adicionamos as luzes e o GameObject das esferas
![passo4](https://github.com/LehLapa/Raycast/assets/128320607/53d7fd68-368a-4a63-8beb-053d8f28950f)

## Imagens de Gameplay
A seguir, as imagens do jogo em execução, podendo observar que as esferas continuam sendo geradas em diversas posições, mesmo quando as anteriores são detruidas. 
![game1](https://github.com/LehLapa/Raycast/assets/128320607/e5b206fc-47c1-4249-852d-c199890fea5c)
![game2](https://github.com/LehLapa/Raycast/assets/128320607/83bee11a-d38f-4f4e-b64f-2515505c11ee)

# Scripts
Para a criação do código seguimos os seguintes passos: 
+ Iniciando o código, declaramos os atributos e métodos que iremos utilizar, como: `Ray`, `RaycastHit`, `Vector3`, `Color` e a Câmera `(public Camera _ camera;)`
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
+ Void Update é aplicado o lançamento do Ray, determinamos que raio será lançado pelo clique do botão direito do mouse e seguindo as cordenadas da posição do mouse na tela. 
Void Lancar, toda a parte de Debug está sendo adicionada. Identificando se o alvo foi atingido e destruído ou não, esclarecendo posição e direção. Caso não tenha sido atingido, será retornado o valor "Target Missed".

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
