using UnityEngine;
using System.Collections;

public abstract class Ennemi : Character {
	
	public int _aggroArea;
	
	public void Invokation() {
		
		ParticuleManager("Invokation");
	}
	
}
