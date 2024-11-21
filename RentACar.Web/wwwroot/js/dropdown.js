document.addEventListener("DOMContentLoaded", () => {
    const dropdownMenu = document.querySelector(".dropdown-menu");
    const selectedOption = document.getElementById("selectedOption");
    const dropdownButton = document.getElementById("carOptionsDropdown");
    const hiddenIdLocationInput = document.getElementById("locationId");
    dropdownMenu.addEventListener("click", (event) => {
        if (event.target.classList.contains("dropdown-item")) {
            const selectedCity = event.target.getAttribute("data-value");
            const selectedCityLocationId = event.target.getAttribute("data-id");
            selectedOption.value = selectedCity;
            hiddenIdLocationInput.value = selectedCityLocationId;
        }
    });
});