/// <binding AfterBuild='watch-bg' />
/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp'),
concat = require("gulp-concat"),
project = require("./project.json"),
browserSync = require('browser-sync');

var paths = {
    webroot: "./" + project.webroot + "/"
};

paths.js = paths.webroot + "js/**/*.js";
paths.cshtml = './**/*.cshtml';
paths.concatJsDest = paths.webroot + "js/site.all.js";

gulp.task('concat', function () {
    // place code for your concat task here
    gulp.src([paths.js, "!" + paths.concatJsDest], { base: "." })
        .pipe(concat(paths.concatJsDest))
        .pipe(gulp.dest("."))
        .pipe(browserSync.reload({
            stream: true
        }));
});

gulp.task('watch', ['browserSync', 'concat'], function () {
    gulp.watch([paths.js, "!" + paths.concatJsDest], ['concat']);
    gulp.watch(paths.cshtml, browserSync.reload);
});

gulp.task('browserSync', function() {
    browserSync({
        proxy: "localhost:6276"
    });
});