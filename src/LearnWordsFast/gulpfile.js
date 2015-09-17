/// <binding AfterBuild='watch-bg' />
/*
 This file in the main entry point for defining Gulp tasks and using Gulp plugins.
 Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
 */

var gulp = require('gulp'),
    del = require('del'),
    webpack = require('webpack'),
    webpackConfig = require('./webpack.config.js'),
    gutil = require('gulp-util'),
    sourcemaps = require('gulp-sourcemaps'),
    sass = require('gulp-sass'),
    livereload = require('gulp-livereload'),
    watch = require('gulp-watch');

gulp.task('clean', function (cb) {
    del(['wwwroot/css/*'], cb);
    console.log('CSS folder was deleted');
});

gulp.task('sass', function () {
    gulp.src('./Client/sass/main.scss')
        .pipe(sourcemaps.init())
        .pipe(sass({errLogToConsole: true}))
        .pipe(sourcemaps.write('./'))
        .pipe(gulp.dest('./wwwroot/css'));
});

gulp.task('watch', function () {
    livereload.listen();
    gulp.watch('./Client/sass/**/*.scss', ['clean', 'sass']).on('change', livereload.changed);
});

gulp.task('webpack', function (callback) {
    webpack(webpackConfig, function (err) {
        if (err) throw new gutil.PluginError("webpack", err);
        callback();
    });
});

gulp.task('watch-sass', ['clean', 'sass', 'watch']);

gulp.task('build-sass', ['clean', 'sass']);
