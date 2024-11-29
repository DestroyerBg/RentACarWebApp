const locationDropdownButton = document.getElementById("carOptionsDropdown");
const locationDropdownItems = document.querySelectorAll("#carOptionsDropdown + .dropdown-menu .dropdown-item");
const selectedLocationInput = document.getElementById("selectedOption");
const locationIdInput = document.getElementById("locationId");

locationDropdownItems.forEach(item => {
    item.addEventListener("click", (event) => {
        event.preventDefault();
        const locationName = item.textContent;
        const locationId = item.getAttribute("data-id");

        locationDropdownButton.textContent = locationName;
        selectedLocationInput.value = locationName;
        locationIdInput.value = locationId;
    });
});