#pragma once

#include "CoreMinimal.h"
#include "GameFramework/Actor.h"
#include "MoverCube.generated.h"

UCLASS()
class UNREALCPP_API AMoverCube : public AActor
{
	GENERATED_BODY()
	
public:
	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = Attributes)
	float moveSpeed = 1.0f;
	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = Attributes)
	FVector randOffsetBounds;

public:	
	AMoverCube();
	virtual void Tick(float DeltaTime) override;

protected:
	virtual void BeginPlay() override;

private:
	void MoveToNearestNeighbour(float DeltaTime);
	void MoveRandomOffset(float DeltaTime);

	bool DistanceCompare(const AActor* a1, const AActor* a2);
};
