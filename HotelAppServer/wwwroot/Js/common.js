ShowToaster = (type, message) => {

    if (type === 'success') {

        toastr.success(message, 'Hotel App', {
            "positionClass": "toast-top-center",

            "closeButton": true,
            "progressBar": true,
            //"showDuration": "5000",
            //"hideDuration": "3000",
            "timeOut": "3000",
            "extendedTimeOut": "3000",
            "showEasing": "swing",
            "hideEasing": "linear",
            /* "showMethod": "fadeIn",*/
            "showMethod": "slideDown",
            /*   "hideMethod": "fadeOut",*/
            "hideMethod": 'slideUp',
            "rtl": false
        });

    }


    if (type === 'error') {

        toastr.error(message, 'Hotel App',
            {
                "positionClass": "toast-top-center",

                "closeButton": true,
                "progressBar": true,
                //"showDuration": "5000",
                //"hideDuration": "3000",
                "timeOut": "3000",
                "extendedTimeOut": "3000",
                "showEasing": "swing",
                "hideEasing": "linear",
                /* "showMethod": "fadeIn",*/
                "showMethod": "slideDown",
                /*   "hideMethod": "fadeOut",*/
                "hideMethod": 'slideUp',
                "rtl": false
            });

    }
}


SwalConfirm = () => {
    return new Promise(resolve => {
        Swal.fire({
            title: 'Are you sure?',
            text: "You won't be able to revert this!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, delete it!'
        }).then((result) => {
            resolve(result.isConfirmed);
        })
    });
}