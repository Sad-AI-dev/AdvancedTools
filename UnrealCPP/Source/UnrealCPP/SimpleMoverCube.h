#pragma once

#include "CoreMinimal.h"
#include "GameFramework/Actor.h"
#include "SimpleMoverCube.generated.h"

UCLASS()
class UNREALCPP_API ASimpleMoverCube : public AActor
{
	GENERATED_BODY()
	
public:
	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = Attributes)
	float moveSpeed = 1.0f;
	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = Attributes)
	FVector randOffsetBounds;

public:	
	ASimpleMoverCube();

	virtual void Tick(float DeltaTime) override;

protected:
	virtual void BeginPlay() override;

private:
	void MoveToNearestNeighbour(float DeltaTime);
	AActor* FindNearestNeighbour(const TArray<AActor*> others) const;
	float GetDistanceToActor(const AActor* other) const;

	void MoveRandomOffset();

};
