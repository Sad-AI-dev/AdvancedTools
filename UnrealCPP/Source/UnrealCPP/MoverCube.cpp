#include "MoverCube.h"

AMoverCube::AMoverCube()
{
	PrimaryActorTick.bCanEverTick = true;
}

//start
void AMoverCube::BeginPlay()
{
	AActor::BeginPlay();
	
}

//update
void AMoverCube::Tick(float DeltaTime)
{
	AActor::Tick(DeltaTime);

}

