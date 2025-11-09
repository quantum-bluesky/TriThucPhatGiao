var gulp = require('gulp');
var postcss = require('gulp-postcss');
var atImport = require('postcss-import');
var autoprefixer = require('autoprefixer');
var cssnano = require('cssnano');

gulp.task('css', function () {
    var plugins = [
        atImport,
        autoprefixer,
        cssnano
    ];
    return gulp.src('./Styles/app.css')
        .pipe(postcss(plugins))
        .pipe(gulp.dest('./wwwroot/css'));
});