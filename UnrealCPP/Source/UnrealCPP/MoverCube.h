#pragma once

#include "CoreMinimal.h"
#include "GameFramework/Actor.h"
#include "MoverCube.generated.h"

UCLASS()
class UNREALCPP_API AMoverCube : public AActor
{
	GENERATED_BODY()
	
public:	
	AMoverCube();

protected:
	virtual void BeginPlay() override;

public:	
	virtual void Tick(float DeltaTime) override;

};
