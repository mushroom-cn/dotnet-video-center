// Generated using webpack-cli https://github.com/webpack/webpack-cli

const path = require("path");
const HtmlWebpackPlugin = require("html-webpack-plugin");
const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const TsconfigPathsPlugin = require("tsconfig-paths-webpack-plugin");
const isProduction = process.env.NODE_ENV == "production";
const CopyPlugin = require("copy-webpack-plugin");
const webpack = require("webpack");
const config = {
  devtool: isProduction ? false : "source-map",
  entry: {
    app: "./src/index.tsx",
  },
  output: {
    path: path.resolve(__dirname, "..", "wwwroot"),
    filename: "[name].[chunkhash:8].js",
    clean: true,
  },
  devServer: {
    open: true,
    host: "localhost",
    compress: true,
    port: 44480,
    // http2: true,
    // https: {
    //   passphrase: "webpack-dev-server",
    //   requestCert: true,
    // },
    static: [
      {
        serveIndex: true,
        directory: path.resolve(__dirname, "..", "wwwroot"),
      },
      {
        directory: path.join(__dirname, "../Temp"),
      },
    ],
  },
  experiments: {
    // futureDefaults: true,
  },
  plugins: [
    new HtmlWebpackPlugin({
      template: "./public/index.html",
      favicon: "./public/favicon.ico",
      manifest: "./public/manifest.json",
    }),
    new webpack.DefinePlugin({
      SERVER_HOST: JSON.stringify("http://localhost:7199"),
    }),
    new CopyPlugin({
      patterns: [
        { from: "public/manifest.json", to: "manifest.json" },
        { from: "public/favicon.ico", to: "favicon.ico" },
      ],
    }),
    new MiniCssExtractPlugin({
      filename: "[id].[chunkhash:8].css",
    }),
    // new TsconfigPathsPlugin({
    //   configFile: path.resolve(__dirname, "./tsconfig.json"),
    // }),
    // Add your plugins here
    // Learn more about plugins from https://webpack.js.org/configuration/plugins/
  ],
  module: {
    rules: [
      {
        test: /\.(ts|tsx)$/i,
        loader: "ts-loader",
        exclude: ["/node_modules/"],
      },
      {
        test: /\.(le|c)ss$/i,
        use: [
          MiniCssExtractPlugin.loader,
          {
            loader: "css-loader",
            options: {
              modules: {
                localIdentName: "[local]",
              },
            },
          },
          "postcss-loader",
          {
            loader: "less-loader",
            options: {
              lessOptions: {
                javascriptEnabled: true,
              },
            },
          },
        ],
      },
      {
        test: /\.(eot|svg|ttf|woff|woff2|png|jpg|gif)$/i,
        type: "asset",
      },

      // Add your rules for custom modules here
      // Learn more about loaders from https://webpack.js.org/loaders/
    ],
  },
  resolve: {
    extensions: [".tsx", ".ts", ".jsx", ".js", ".less", ".json", ".css"],
    plugins: [
      new TsconfigPathsPlugin({
        /* options: see below */
        configFile: path.resolve(__dirname, "./tsconfig.json"),
      }),
    ],
  },
  optimization: {
    // chunkIds: "named",
    splitChunks: {
      chunks: "all",
      cacheGroups: {
        defaultVendors: {
          // reuseExistingChunk: true,
          test: /[\\/]node_modules[\\/]/,
          filename: "[name].bundle.[chunkhash:8].js",
          name: "vendor",
          chunks: "all",
        },
      },
    },
  },
};

module.exports = () => {
  if (isProduction) {
    config.mode = "production";
  } else {
    config.mode = "development";
  }
  return config;
};
