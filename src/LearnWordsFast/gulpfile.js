/// <binding AfterBuild='watch-bg' />
/*
 This file in the main entry point for defining Gulp tasks and using Gulp plugins.
 Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
 */

var gulp = require('gulp'),
    webpack = require('webpack'),
    webpackConfig = require('./webpack.config.js');

gulp.task('webpack', function (callback) {
    webpack(webpackConfig, function (err) {
        if (err) throw new gutil.PluginError("webpack", err);
        callback();
    });
});