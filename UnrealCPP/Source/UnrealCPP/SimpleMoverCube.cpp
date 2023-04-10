#include "SimpleMoverCube.h"

#include "Kismet/GameplayStatics.h"

// Sets default values
ASimpleMoverCube::ASimpleMoverCube()
{
 	PrimaryActorTick.bCanEverTick = true;
}

//start
void ASimpleMoverCube::BeginPlay()
{
	Super::BeginPlay();
	
}

//update
void ASimpleMoverCube::Tick(float DeltaTime)
{
	Super::Tick(DeltaTime);
	//mover cube behaviour
	MoveToNearestNeighbour(DeltaTime);
	MoveRandomOffset();
}

void ASimpleMoverCube::MoveToNearestNeighbour(float DeltaTime) {
	//find all actors of type
	TArray<AActor*> movers;
	UWorld* world = GetWorld();
	if (world) {
		UGameplayStatics::GetAllActorsOfClass(world, ASimpleMoverCube::StaticClass(), movers);
		//remove close movers
		for (int i = movers.Num() - 1; i >= 0; i--) {
			if ((GetActorLocation() - movers[i]->GetActorLocation()).Length() < 10.0f) {
				movers.RemoveAt(i);
			}
		}
		if (movers.Num() > 0) {
			//sort list based on distance
			AActor* closest = FindNearestNeighbour(movers);
			//move towards nearest
			FVector moveDir = closest->GetActorLocation() - GetActorLocation();
			SetActorLocation(GetActorLocation() + (moveDir * (moveSpeed * DeltaTime)));
		}
	}
}

AActor* ASimpleMoverCube::FindNearestNeighbour(const TArray<AActor*> others) const {
	int closest = 0;
	float closestDistance = GetDistanceToActor(others[0]);
	//go through entire list, start at 1, since 0 is used as default
	for (int i = 1; i < others.Num(); i++) {
		float dist = GetDistanceToActor(others[i]);
		if (dist < closestDistance) {
			closest = 0;
			closestDistance = dist;
		}
	}
	return others[closest];
}

float ASimpleMoverCube::GetDistanceToActor(const AActor* other) const {
	return FVector::Distance(GetActorLocation(), other->GetActorLocation());
}

void ASimpleMoverCube::MoveRandomOffset() {
	SetActorLocation(GetActorLocation() + FVector(
		FMath::RandRange(-randOffsetBounds.X, randOffsetBounds.X),
		FMath::RandRange(-randOffsetBounds.Y, randOffsetBounds.Y),
		FMath::RandRange(-randOffsetBounds.Z, randOffsetBounds.Z)
	));
}