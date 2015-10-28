var pkg = require("./package.json"),
    project = require("./project.json"),
    webpack = require('webpack');

module.exports = {
    entry: {
        app: ['./Client/js/app.jsx'],
        vendor: Object.keys(pkg.dependencies)
    },
    output: {
        path: "./wwwroot/js",
        filename: 'scripts.[name].js'
    },
    devtool: 'sourcemap',
    plugins: [
        new webpack.optimize.CommonsChunkPlugin("vendor", "scripts.vendor.js")
    ],
    resolve: {
        extensions: ['.js', '.jsx', '']
    },
    module: {
        preLoaders: [
            {
                test: /.js.?$/,
                exclude: /(node_modules|bower_components|wwwroot)/,
                loader: 'eslint-loader'
            }
        ],
        loaders: [
            {
                test: /.js.?$/,
                exclude: /(node_modules|bower_components|wwwroot)/,
                loader: 'babel?cacheDirectory&optional=es7.objectRestSpread'
            }
        ]
    }
};
