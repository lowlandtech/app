module.exports = {
  name: 'settings',
  preset: '../../jest.config.js',
  coverageDirectory: '../../coverage/libs/settings',
  snapshotSerializers: [
    'jest-preset-angular/AngularSnapshotSerializer.js',
    'jest-preset-angular/HTMLCommentSerializer.js'
  ]
};
