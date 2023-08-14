window.addEventListener("load", solve);

function solve() {
    const nextButton = document.getElementById("next-btn")
    const previewList = document.getElementById("preview-list")
    const appliedList = document.getElementById("candidates-list")
    nextButton.addEventListener("click", (e) =>{
    const studentElement = document.getElementById("student")
    const uniElement = document.getElementById("university")
    const scoreElement = document.getElementById("score")
    const student = studentElement.value
    const uni = uniElement.value
    const score = scoreElement.value
    if (student === "" || uni === "" || score === ""){
      return
    }
    const listElement = document.createElement("li")
    listElement.className = "application"
    const articleElement = document.createElement("article")
    const nameElement = document.createElement("h4")
    nameElement.textContent = `${student}`
    const uniParagraph = document.createElement("p")
    uniParagraph.textContent = `University: ${uni}`
    const scoreParagraph = document.createElement("p")
    scoreParagraph.textContent = `Score: ${score}`
    articleElement.appendChild(nameElement)
    articleElement.appendChild(uniParagraph)
    articleElement.appendChild(scoreParagraph)
    const editButton = document.createElement("button")
    editButton.className = "action-btn edit"
    editButton.textContent = "edit"
    const applyButton = document.createElement("button")
    applyButton.className = "action-btn apply"
    applyButton.textContent = "apply"

    listElement.appendChild(articleElement)
    listElement.appendChild(editButton)
    listElement.appendChild(applyButton)
    previewList.appendChild(listElement)
    editButton.addEventListener("click", (e) =>{
      studentElement.value = student
      uniElement.value = uni
      scoreElement.value = score
      nextButton.disabled = false
      listElement.remove()
    })
    applyButton.addEventListener("click", (e) =>{
      listElement.remove()
      appliedList.appendChild(listElement)
      editButton.remove()
      applyButton.remove()
      nextButton.disabled = false
    })
    studentElement.value = ""
    uniElement.value = ""
    scoreElement.value = ""
    nextButton.disabled = true

  })
}
  