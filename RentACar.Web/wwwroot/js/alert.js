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