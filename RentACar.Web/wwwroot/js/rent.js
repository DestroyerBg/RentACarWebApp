document.addEventListener("DOMContentLoaded", () => {
    const startDateInput = document.getElementById("startDate");
    const endDateInput = document.getElementById("endDate");
    const checkboxes = document.querySelectorAll(".form-check-input");
    const totalPriceInput = document.getElementById("car-price");
    const basePriceInput = document.getElementById("carPricePerDay");
    const basePricePerDay = Number(basePriceInput.value);

    const calculateTotalPrice = () => {

        if (!startDateInput.value || !endDateInput.value) {
            alert("Моля, въведете валидни дати");
            return;
        }

        const startDate = ParseDate(startDateInput);
        const endDate =  ParseDate(endDateInput);

        if (startDate >= endDate) {
            alert("Невалидни стойности на датите!");
            return;
        }

        const timeDiff = endDate - startDate;
        const days = Math.ceil(timeDiff / (1000 * 60 * 60 * 24));

        
        let totalPrice = days * basePricePerDay;

        checkboxes.forEach((checkbox) => {
            if (checkbox.checked) {
                totalPrice += parseInt(checkbox.value) * days;
            }
        });

        totalPriceInput.textContent = `${totalPrice.toFixed(2)} лева.`;
    };

    endDateInput.addEventListener("change", calculateTotalPrice);
    checkboxes.forEach((checkbox) =>
        checkbox.addEventListener("change", calculateTotalPrice)
    );
});

function ParseDate(dateInput) {
    const [day, month, year] = dateInput.value.split(".").map(Number);
    return new Date(year, month - 1, day);
}