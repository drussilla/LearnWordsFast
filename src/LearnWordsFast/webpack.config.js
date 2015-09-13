var pkg = require("./package.json"),
    project = require("./project.json"),
    webpack = require('webpack');

module.exports = {
    entry: {
        app: ['./Client/js/main.jsx'],
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
        loaders: [
            {
                test: /\.jsx?$/,
                exclude: /(node_modules|bower_components)/,
                loader: 'babel?cacheDirectory&optional=es7.objectRestSpread'
            }
        ]
    }
};

