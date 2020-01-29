module.exports = {
  name: 'content',
  preset: '../../jest.config.js',
  coverageDirectory: '../../coverage/libs/content',
  snapshotSerializers: [
    'jest-preset-angular/AngularSnapshotSerializer.js',
    'jest-preset-angular/HTMLCommentSerializer.js'
  ]
};
