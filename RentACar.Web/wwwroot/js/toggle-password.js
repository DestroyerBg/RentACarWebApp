document.addEventListener("DOMContentLoaded", () => {
    const togglePasswordButtons = document.querySelectorAll(".toggle-password");

    togglePasswordButtons.forEach((button) => {
        button.addEventListener("click", () => {
            const targetInput = document.querySelector(button.dataset.target);
            const icon = button.querySelector("i");

            if (targetInput.type === "password") {
                targetInput.type = "text"; 
                icon.classList.remove("fa-eye");
                icon.classList.add("fa-eye-slash");
            } else {
                targetInput.type = "password"; 
                icon.classList.remove("fa-eye-slash");
                icon.classList.add("fa-eye");
            }
        });
    });
});