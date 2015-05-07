#pragma strict

function Start () {

}

function Update () {

}

function OnTriggerEnter(obj:Collider) {
    
    if(obj.gameObject.name == "Spi") {
        Application.LoadLevel( "map_boss_veryBadBoy" );
    }
}