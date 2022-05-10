using System.Collections.Generic;
using __Common.Extensions;
using __Common.ObjectPooling;
using UnityEngine;
using Random = System.Random;

namespace AgarioRipoff.FoodSystem
{
    public class FoodSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject foodPool;
        [SerializeField] private int initialFoodAmount;
        
        [SerializeField] private Camera mainCam;
        [SerializeField] private GameObject foodContainer;

        [SerializeField] private GameObject player;
        [SerializeField] private GameObject joystickBox;

        [SerializeField] private float defaultGrowAmount;

        [SerializeField] private int foodThreshold;

        private ObjectPool _foodPoolScript;

        private void Awake()
        {
            _foodPoolScript = foodPool.GetComponent<ObjectPool>();
        }

        private void Start()
        {
            // We spawn all the initial food.
            for (int i = 0; i < initialFoodAmount; i++)
            {
                SpawnFood(i);
            }
        }

        private void Update()
        {
            int currentFood = foodContainer.transform.childCount;

            if (currentFood >= foodThreshold)
                return;

            for (int i = 0; i < 10; i++)
            {
                SpawnFood(i);
            }
        }

        private void SpawnFood(int i)
        {
            // Rect playerLoc = player.GetComponent<RectTransform>().rect;

            Vector2 screenLocation = mainCam.GetRandomScreenLocation(new List<Transform>{joystickBox.transform});

            Random rnd = new Random();

            double growExtra = rnd.NextDouble() / 100;

            if (i % 2 == 0)
                growExtra = -growExtra;
                
            float growAmount = defaultGrowAmount + (float)growExtra;
                
            FoodBehaviour food = _foodPoolScript.GetItemFromPool(screenLocation, new Quaternion(0, 0, 0, 0), foodContainer.transform).GetComponent<FoodBehaviour>();
                
            food.Initialize(growAmount);
        }
    }
}
