#include "MoverCube.h"

#include "Kismet/GameplayStatics.h"

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
	//mover cube behaviour
	MoveToNearestNeighbour(DeltaTime);
	MoveRandomOffset(DeltaTime);
}

void AMoverCube::MoveToNearestNeighbour(float DeltaTime) {
	//find all actors of type
	TArray<AActor*> movers;
	UWorld* world = GetWorld();
	if (world) {
		UGameplayStatics::GetAllActorsOfClass(world, AMoverCube::StaticClass(), movers);
		//remove close movers
		for (int i = movers.Num() - 1; i >= 0; i--) {
			if ((GetActorLocation() - movers[i]->GetActorLocation()).Length() < 10.0f) {
				movers.RemoveAt(i);
			}
		}
		if (movers.Num() > 0) {
			//sort list based on distance
			Algo::Sort(movers, [this](const AActor* a1, const AActor* a2) {
				return DistanceCompare(a1, a2);
			});
			//move towards nearest
			FVector moveDir = movers[0]->GetActorLocation() - GetActorLocation();
			SetActorLocation(GetActorLocation() + (moveDir * (moveSpeed * DeltaTime)));
		}
	}
}

void AMoverCube::MoveRandomOffset(float DeltaTime) {
	SetActorLocation(GetActorLocation() + FVector(
		FMath::RandRange(-randOffsetBounds.X, randOffsetBounds.X),
		FMath::RandRange(-randOffsetBounds.Y, randOffsetBounds.Y),
		FMath::RandRange(-randOffsetBounds.Z, randOffsetBounds.Z)
		));
}

//============ sort helper func =============
bool AMoverCube::DistanceCompare(const AActor* a1, const AActor* a2) {
	return (GetActorLocation() - a1->GetActorLocation()).Length() < (GetActorLocation() - a2->GetActorLocation()).Length();
}