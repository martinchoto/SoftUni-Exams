const taskUrl = "http://localhost:3030/jsonstore/tasks"
const nameInput = document.getElementById("name")
const numberOfDays = document.getElementById("num-days")
const fromDate = document.getElementById("from-date")
const addButton = document.getElementById("add-vacation")
const editButton = document.getElementById("edit-vacation")
const loadButton = document.getElementById("load-vacations")
const list = document.getElementById("list")
let vacationId = ""
loadButton.addEventListener("click", loadVacations())
async function loadVacations(){
    list.innerHTML = ""
    const response = await fetch(taskUrl)
    const data = await response.json()
    const vacations = Object.values(data)
    for (const vacation of vacations) {
        createVacation(vacation)
    }
}
addButton.addEventListener("click", async (e) =>{
    e.preventDefault()
    const name = nameInput.value
    const days = numberOfDays.value
    const date = fromDate.value
    const vacation = {
        name,
        days,
        date
    }
    await fetch(taskUrl, {
        method: "POST",
        body: JSON.stringify(vacation)
    })
    nameInput.value = ""
    numberOfDays.value = ""
    fromDate.value = ""
    await loadVacations()
})
editButton.addEventListener("click", async (e) =>{
    e.preventDefault()
    console.log(vacationId)
    const name = nameInput.value
    const days = numberOfDays.value
    const date = fromDate.value
    const vacation = {
        name,
        days,
        date,
        _id: vacationId
    }
    await fetch(`${taskUrl}/${vacationId}`, {
        method: "PUT",
        body: JSON.stringify(vacation)
    })
    nameInput.value = ""
    numberOfDays.value = ""
    fromDate.value = ""
    editButton.disabled = true
    addButton.disabled = false
    await loadVacations()
})
function createVacation(vacation){
    const divElement = document.createElement("div")
    divElement.className = "container"
    divElement.setAttribute("data-id", vacation._id)
    const nameOfPerson = document.createElement("h2")
    nameOfPerson.textContent = vacation.name
    const dateOfRes = document.createElement("h3")
    dateOfRes.textContent = vacation.date
    const daysOfVac = document.createElement("h3")
    daysOfVac.textContent = vacation.days
    const changeButton = document.createElement("button")
    changeButton.className = "change-btn"
    changeButton.textContent = "Change"

    changeButton.addEventListener("click", async (e) =>{
        e.preventDefault()
        vacationId = divElement.getAttribute("data-id")
        editButton.disabled = false
        addButton.disabled = true
        nameInput.value = vacation.name
        numberOfDays.value = vacation.days
        fromDate.value = vacation.date
        divElement.remove()
    })

    const doneButton = document.createElement("button")
    doneButton.className = "done-btn"
    doneButton.textContent = "Done"

    doneButton.addEventListener("click", async (e) =>{
        e.preventDefault()
        const elementId = divElement.getAttribute("data-id")
        await fetch(`${taskUrl}/${elementId}`, {
            method: "DELETE"
        })
        await loadVacations()
    })
    divElement.appendChild(nameOfPerson)
    divElement.appendChild(dateOfRes)
    divElement.appendChild(daysOfVac)
    divElement.appendChild(changeButton)
    divElement.appendChild(doneButton)

    list.appendChild(divElement)
}