module.exports = {
  name: 'card',
  preset: '../../jest.config.js',
  coverageDirectory: '../../coverage/libs/card',
  snapshotSerializers: [
    'jest-preset-angular/AngularSnapshotSerializer.js',
    'jest-preset-angular/HTMLCommentSerializer.js'
  ]
};
