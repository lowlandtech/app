module.exports = {
  name: 'header',
  preset: '../../jest.config.js',
  coverageDirectory: '../../coverage/libs/header',
  snapshotSerializers: [
    'jest-preset-angular/AngularSnapshotSerializer.js',
    'jest-preset-angular/HTMLCommentSerializer.js'
  ]
};
