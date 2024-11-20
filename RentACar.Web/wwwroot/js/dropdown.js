document.addEventListener("DOMContentLoaded", () => {
    const dropdownMenu = document.querySelector(".dropdown-menu");
    const selectedOption = document.getElementById("selectedOption");
    const dropdownButton = document.getElementById("carOptionsDropdown");

    dropdownMenu.addEventListener("click", (event) => {
        if (event.target.classList.contains("dropdown-item")) {
            const selectedCity = event.target.getAttribute("data-value"); 
            selectedOption.value = selectedCity; 
        }
    });
});