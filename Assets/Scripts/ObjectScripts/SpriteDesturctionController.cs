using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteDesturctionController : MonoBehaviour
{
	void Start() {
		Explodable explodable = GetComponent<Explodable>();
		explodable.explode();
	}
}
