const path = require('path');

module.exports = async ({ config, mode }) => {
  config.module.rules.push({
    test: [/\.stories\.tsx?$/, /index\.ts$/],
    loaders: [
      {
        loader: require.resolve('@storybook/source-loader'),
        options: {
          parser: 'typescript'
        }
      }
    ]
  });
  return config;
};
