function PrintAlert(message) {
    if (message) {
        Swal.fire({
            icon: 'success',
            title: 'Успех',
            text: message,
            background: '#343a40',
            color: '#f8f9fa',
            confirmButtonColor: '#6c757d'
        });
    }
}

function ConfirmAction(actionUrl, carBrandAndModel) {
    Swal.fire({
        title: `Сигурen ли сu, че искаш да изтриеш ${carBrandAndModel}?`,
        text: "Тази операция не може да бъде отменена!",
        icon: 'warning',
        background: '#343a40',
        color: '#f8f9fa',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#3085d6',
        confirmButtonText: 'Да, продължи',
        cancelButtonText: 'Отказ'
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = actionUrl;
        }
    });
}