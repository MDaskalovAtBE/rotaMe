'use strict';
$(document).ready(function() {
    // [ sweet-basic ]
    $('.sweet-basic').on('click', function() {
        swal('Hello world!');
    });
    // [ sweet-success ]
    $('.sweet-success').on('click', function() {
        swal("Good job!", "You clicked the button!", "success");
    });
    // [ sweet-warning ]
    $('.sweet-warning').on('click', function() {
        swal("Good job!", "You clicked the button!", "warning");
    });
    // [ sweet-error ]
    $('.sweet-error').on('click', function() {
        swal("Good job!", "You clicked the button!", "error");
    });
    // [ sweet-info ]
    $('.sweet-info').on('click', function() {
        swal("Good job!", "You clicked the button!", "info");
    });
    // [ sweet-multiple ]
    $('.sweet-multiple').on('click', function () {
        swal({
            title: "Are you sure?",
            text: "Once deleted, you will not be able to recover this imaginary file!",
            icon: "warning",
            buttons: true,
            dangerMode: true,
        })
            .then((willDelete) => {
                if (willDelete) {
                    swal("Poof! Your imaginary file has been deleted!", {
                        icon: "success",
                    });
                } else {
                    swal("Your imaginary file is safe!", {
                        icon: "error",
                    });
                }
            });
    });
    // [ sweet-multiple-delete-user ]
    $('.sweet-multiple-delete-user').on('click', function (event) {
        event.preventDefault();
        swal({
            title: "Are you sure?",
            text: "Once deleted, you will not be able to recover this user!",
            icon: "warning",
            buttons: true,
            dangerMode: true,
        })
            .then((willDelete) => {
                if (willDelete) {
                    $.ajax({
                        type: "POST",
                        url: this.href,
                        success: function () {
                            swal("Poof! The user has been deleted!", {
                                icon: "success",
                            }).then(() => {
                                location.reload();
                            });
                        },
                        error: function () {
                            swal("The user hasn't been deleted!", {
                                icon: "error",
                            });
                        },
                    })

                } else {
                    swal("The user hasn't been deleted!", {
                        icon: "error",
                    });

                }
            });
    });
    // [ sweet-multiple-delete-role ]
    $('.sweet-multiple-delete-role').on('click', function (event) {
        event.preventDefault();
        swal({
            title: "Are you sure?",
            text: "Once deleted, you will not be able to recover this role!",
            icon: "warning",
            buttons: true,
            dangerMode: true,
        })
            .then((willDelete) => {
                if (willDelete) {
                    $.ajax({
                        type: "POST",
                        url: this.href,
                        success: function () {
                            swal("Poof! The role has been deleted!", {
                                icon: "success",
                            }).then(() => {
                                location.reload();
                            });
                        },
                        error: function () {
                            swal("The role hasn't been deleted!", {
                                icon: "error",
                            });
                        },
                    })
                } else {
                    swal("The role hasn't been deleted!", {
                        icon: "error",
                    });

                }
            });
    });
    // [ sweet-multiple-delete-project ]
    $('.sweet-multiple-delete-project').on('click', function (event) {
        event.preventDefault();
        swal({
            title: "Are you sure?",
            text: "Once deleted, you will not be able to recover this project!",
            icon: "warning",
            buttons: true,
            dangerMode: true,
        })
            .then((willDelete) => {
                if (willDelete) {
                    $.ajax({
                        type: "POST",
                        url: this.href,
                        success: function () {
                            swal("Poof! The project has been deleted!", {
                                icon: "success",
                            }).then(() => {
                                location.reload();
                            });
                        },
                        error: function () {
                            swal("The project hasn't been deleted!", {
                                icon: "error",
                            });
                        },
                    })
                } else {
                    swal("The project hasn't been deleted!", {
                        icon: "error",
                    });

                }
            });
    });
    // [ sweet-prompt ]
    $('.sweet-prompt').on('click', function() {
        swal("Write something here:", {
                content: "input",
            })
            .then((value) => {
                swal(`You typed: ${value}`);
            });
    });
    // [ sweet-ajax ]
    $('.sweet-ajax').on('click', function() {
        swal({
                text: 'Search for a movie. e.g. "La La Land".',
                content: "input",
                button: {
                    text: "Search!",
                    closeModal: false,
                },
            })
            .then(name => {
                if (!name) throw null;
                return fetch(`https://itunes.apple.com/search?term=${name}&entity=movie`);
            })
            .then(results => {
                return results.json();
            })
            .then(json => {
                const movie = json.results[0];
                if (!movie) {
                    return swal("No movie was found!");
                }
                const name = movie.trackName;
                const imageURL = movie.artworkUrl100;
                swal({
                    title: "Top result:",
                    text: name,
                    icon: imageURL,
                });
            })
            .catch(err => {
                if (err) {
                    swal("Oh noes!", "The AJAX request failed!", "error");
                } else {
                    swal.stopLoading();
                    swal.close();
                }
            });
    });
});
