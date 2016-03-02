var moviesModule = angular.module("movieApp", ['ngAnimate', 'toastr']);

moviesModule.controller("moviesCtrl", ["$scope", "$http", "toastr",
    function ($scope, $http, toastr) {

        // ---- HTML models -----
        // Just to show I understand the most basic of binding, why not :P
        $scope.header = "My super-awesome-have-to-watch movie list";
        $scope.movies = [];
        $scope.genresForSelect = [];
        $scope.selectedGenres = [];
        $scope.title;

        // ---- Functions ----

        // Retieve all stored Movies
        $scope.getMovies = function () {

            $http.get("/api/movies/get", null)
              .then(function (response) { //Successful return    
                  // Error returned
                  if (response.status !== 200) {
                      toastr.error(response.statusText, "" + response.status);
                  }

                  // Successful return                 
                  var movies = response.data;
                  // create an object to hold the formatted list data
                  var formattedMovies = [];

                  // Loop thorugh all returned movies and format them to be display friendly
                  $.each(movies, function (i, movie) {
                      // Create a temp movie object to hold formatted vals
                      var formattedMovie = { title: movie.title, genres: "", released: "" };

                      // Loop through the genres for each movie, formatting them to a display string
                      $.each(movie.genres, function (i, genre) {
                          // Make sure to not add commas to the first and last list item
                          if (i == movie.genres.length - 1) {
                              formattedMovie.genres += genre.name;
                          }
                          else {
                              formattedMovie.genres += genre.name + ", ";
                          }
                      });

                      // Format the date to make it display friendly
                      formattedMovie.released = formatDateFriendly(movie.released);

                      // Push the formatted object to the display friendly list
                      formattedMovies.push(formattedMovie);
                  });

                  // Bind the formatted list to the table model
                  $scope.movies = formattedMovies;

              }, function (response) {  //Error connecting to web service
                  toastr.error(response.statusText, "" + response.status);
              });
        }

        // Retrieve all Genres in the DB
        $scope.getGenres = function () {
            $http.get("/api/genres/get", null)
              .then(function (response) { //Successful return    
                  // Error returned
                  if (response.status !== 200) {
                      toastr.error(response.statusText, "" + response.status);
                  }

                  // Successful return                 
                  var genres = response.data;

                  $scope.genresForSelect = {
                      genreOptions: response.data,
                      selectedGenre: { id: genres[0].id, name: genres[0].name } //This sets the default value of the select in the ui
                  };

              }, function (response) {  //Error connecting to web service
                  toastr.error(response.statusText, "" + response.status);
              });
        }

        // Add genre to selected genres
        $scope.addGenre = function () {
            $scope.selectedGenres.push($scope.genresForSelect.selectedGenre);

            // Get the indexes of the genre select array
            var indexes = $.map($scope.genresForSelect.genreOptions, function (obj, index) {
                if (obj.id == $scope.genresForSelect.selectedGenre.id) {
                    return index;
                }
            });

            // Use the index (will always be the first, and only) to remove genre from the select
            $scope.genresForSelect.genreOptions.splice(indexes[0], 1);

            // Reset the default selected option
            $scope.genresForSelect.selectedGenre = $scope.genresForSelect.genreOptions[0];

        }

        // Remove genre from selected genres
        $scope.removeSelectedGenre = function (genre) {
            $scope.genresForSelect.genreOptions.push({ id: genre.id, name: genre.name });

            // Get the indexes of the genre select array
            var indexes = $.map($scope.selectedGenres, function (obj, index) {
                if (obj.id == genre.id) {
                    return index;
                }
            });

            // Use the index (will always be the first, and only) to remove genre from the select
            $scope.selectedGenres.splice(indexes[0], 1);

            // Reset the default selected option
            $scope.genresForSelect.selectedGenre = $scope.genresForSelect.genreOptions[0];
        }

        // Add new movie
        $scope.addMovie = function () {
            var dateReleased = $("#released").datepicker('getUTCDate');

            var newMovie = {
                Title: $scope.title,
                Released: dateReleased,
                GenreIds: []
            }

            $.each($scope.selectedGenres, function (i, genre) {
                newMovie.GenreIds.push(genre.id);
            });

            $.ajax({
                type: "POST",
                contentType: "application/json;charset=utf-8",
                url: "/api/movies/post",
                data: JSON.stringify(newMovie),
                success: function (response) {
                    //check if an error occured
                    if ((typeof response == "string")) // Will be an error if plain string is returned
                    {
                        toastr.error(response, "Attention", "#");
                        return false;;
                    }

                    // Refresh table
                    $scope.getMovies();

                    // Clear fields
                    $scope.title = "";
                    $("#released").datepicker('clearDates');
                    $scope.selectedGenres = [];
                    $scope.genresForSelect = {};
                    // refresh genres
                    $scope.getGenres();

                    // Notify user
                    toastr.success("Updated successfully");

                },
                error: function (a, e, d) {
                    toastr.error(e + ' ' + d, "Service Error", "#");
                    return;
                }
            });
        }

        // ---- Init
        $scope.getGenres();
        $scope.getMovies();
        $(".datepicker").datepicker({
            autoClose: true,
            startView: 'decade',
            orientation: 'bottom',
            format: "dd/mm/yyyy"
        });


    }]);



// Global functions

// Function to format a date object to a display friendly date string
function formatDateFriendly(unformattedDate) {
    // handle null dates
    if (!unformattedDate) {
        return "Not specified";
    }

    var monthNames = [
        "January", "February", "March",
        "April", "May", "June", "July",
        "August", "September", "October",
        "November", "December"
    ];

    var date = new Date(unformattedDate);
    var day = date.getDate();
    var monthIndex = date.getMonth();
    var year = date.getFullYear();

    return day + ' ' + monthNames[monthIndex] + ' ' + year;
}
