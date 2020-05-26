module.exports = {
  name: 'playground',
  preset: '../../jest.config.js',
  coverageDirectory: '../../coverage/libs/playground',
  snapshotSerializers: [
    'jest-preset-angular/AngularSnapshotSerializer.js',
    'jest-preset-angular/HTMLCommentSerializer.js'
  ]
};
