/// <binding BeforeBuild='default' />
/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp');
rimraf = require("rimraf"),
concat = require("gulp-concat");
project = require("./project.json");

var paths = {
    webroot: "./" + project.webroot + "/"
};

paths.js = paths.webroot + "js/*.js";
paths.concatJsDest = paths.webroot + "js/site.all.js";

gulp.task('clean', function(cb) {
    rimraf(paths.concatJsDest, cb);
});

gulp.task('default', function () {
    // place code for your default task here
    gulp.src([paths.js, "!" + paths.concatJsDest], { base: "." })
        .pipe(concat(paths.concatJsDest))
        .pipe(gulp.dest("."));
});