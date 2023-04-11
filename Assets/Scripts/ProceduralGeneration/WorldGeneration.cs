using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGeneration : MonoBehaviour {
    [SerializeField] private GameObject _block;
    private Vector3 startPos = new Vector3(-10, 0, -10);
    void Start(){
        for (int i = 0; i < 20; i++) {
            for (int j = 0; j < 20; j++) {
                GameObject block = Instantiate(_block) as GameObject;
                block.transform.position = startPos + new Vector3(i, 0, j);
            }
        }
    }
}
