document.addEventListener("DOMContentLoaded", () => {
    const filterForm = document.querySelector("#filterForm");

    filterForm.addEventListener("submit", (e) => {
        e.preventDefault();

        const selectedFilters = Array.from(filterForm.querySelectorAll(".form-check-input:checked"))
            .map((checkbox) => checkbox.value);

    });
});