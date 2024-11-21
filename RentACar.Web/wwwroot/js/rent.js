document.addEventListener("DOMContentLoaded", () => {
    const startDateInput = document.getElementById("startDate");
    const endDateInput = document.getElementById("endDate");
    const checkboxes = document.querySelectorAll(".form-check-input");
    const totalPriceInput = document.getElementById("totalPrice");
    const basePriceInput = document.getElementById("carPricePerDay");
    const basePricePerDay = Number(basePriceInput.value);

    const calculateTotalPrice = () => {
        const startDate = ParseDate(startDateInput);
        const endDate = ParseDate(endDateInput);

        if (!startDate || !endDate || isNaN(startDate) || isNaN(endDate)) {
            totalPriceInput.textContent = "Моля, въведете валидни дати!";
            return;
        }

        if (startDate >= endDate) {
            totalPriceInput.textContent = "Началната дата трябва да е преди крайната!";
            return;
        }

        const timeDiff = endDate - startDate;
        const days = Math.ceil(timeDiff / (1000 * 60 * 60 * 24));

        let totalPrice = days * basePricePerDay;

        checkboxes.forEach((checkbox) => {
            if (checkbox.checked) {
                totalPrice += Number(checkbox.value).toFixed(2) * days;
            }
        });

        totalPriceInput.textContent = `${totalPrice.toFixed(2)} лева.`;
    };

    startDateInput.addEventListener("change", calculateTotalPrice);
    endDateInput.addEventListener("change", calculateTotalPrice);
    checkboxes.forEach((checkbox) =>
        checkbox.addEventListener("change", calculateTotalPrice)
    );
});

function ParseDate(dateInput) {
    if (!dateInput.value) return null; 
    const [day, month, year] = dateInput.value.split(".").map(Number);

    if (!day || !month || !year || isNaN(day) || isNaN(month) || isNaN(year)) {
        return null; 
    }

    return new Date(year, month - 1, day);
}
