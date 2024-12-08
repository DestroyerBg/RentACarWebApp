document.addEventListener('DOMContentLoaded', function () {
    const deleteButtons = document.querySelectorAll('.delete-feedback-btn');

    deleteButtons.forEach(button => {
        button.addEventListener('click', function () {
            const feedbackId = this.getAttribute('data-feedback-id');

            Swal.fire({
                title: 'Сигурни ли сте, че искате да изтриете това мнение?',
                text: 'Това действие не може да бъде отменено!',
                icon: 'warning',
                background: '#343a40',
                color: '#f8f9fa',
                showCancelButton: true,
                confirmButtonColor: '#d33',
                cancelButtonColor: '#3085d6',
                confirmButtonText: 'Да, изтрий!',
                cancelButtonText: 'Отказ'
            }).then((result) => {
                if (result.isConfirmed) {
                    fetch(`/CustomerFeedback/DeleteFeedback/?id=${feedbackId}`, {
                        method: 'POST',
                        headers: {
                            'Content-Type': 'application/json'
                        }
                    }).then(response => response.json())
                        .then(data => {
                            if (data.success == true) {
                                PrintAlert(data.message);
                                button.closest('.list-group-item').remove();
                            } else {
                                PrintError(data.message);
                            }
                        });
                }
            });
        });
    });
});