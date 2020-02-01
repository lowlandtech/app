module.exports = {
  name: 'profile',
  preset: '../../jest.config.js',
  coverageDirectory: '../../coverage/libs/profile',
  snapshotSerializers: [
    'jest-preset-angular/AngularSnapshotSerializer.js',
    'jest-preset-angular/HTMLCommentSerializer.js'
  ]
};
