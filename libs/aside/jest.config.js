module.exports = {
  name: 'aside',
  preset: '../../jest.config.js',
  coverageDirectory: '../../coverage/libs/aside',
  snapshotSerializers: [
    'jest-preset-angular/AngularSnapshotSerializer.js',
    'jest-preset-angular/HTMLCommentSerializer.js'
  ]
};
