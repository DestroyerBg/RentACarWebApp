document.addEventListener("DOMContentLoaded", () => {
    function restrictToOne(groupName) {
        const checkboxes = document.querySelectorAll(`.${groupName}`);

        checkboxes.forEach(checkbox => {
            checkbox.addEventListener("change", () => {
                if (checkbox.checked) {
                    checkboxes.forEach(otherCheckbox => {
                        if (otherCheckbox !== checkbox) {
                            otherCheckbox.checked = false;
                        }
                    });
                }
            });
        });
    }

    restrictToOne("category-checkbox");

    restrictToOne("location-checkbox");
});