function solve(input)
{
    let riders = {}
    const n = Number(input.shift())
    for (let i = 0; i < n; i++){
        let [name, fuelCapacity, position] = input.shift().split("|")
        riders[name] = {fuelCapacity, position}
    }
    let line = input.shift()
    while (line !== "Finish"){
        let [command, name, thirdElement, changedPosition] = line.split(" - ")
        if (command === "StopForFuel"){
            if (riders[name].fuelCapacity < Number(thirdElement)){
                riders[name].position = changedPosition
                console.log(`${name} stopped to refuel but lost his position, now he is ${changedPosition}.`)
            }else {
                console.log(`${name} does not need to stop for fuel!`)
            }
        }else if (command === "Overtaking"){
            if (riders[name].position < riders[thirdElement].position){
                let temp = riders[name].position
                riders[name].position = riders[thirdElement].position
                riders[thirdElement].position = temp
                console.log(`${name} overtook ${thirdElement}!`)
            }
        }else if (command === "EngineFail"){            
            delete riders[name]
            console.log(`${name} is out of the race because of a technical issue, ${thirdElement} laps before the finish.`)
        }
        line = input.shift()
    }
    let entries = Object.entries(riders)
    for (let [key, value] of entries) {
        console.log(key)
        console.log(` Final position: ${value.position}`)
    }
}