module.exports = {
  name: 'card-list',
  preset: '../../jest.config.js',
  coverageDirectory: '../../coverage/libs/card-list',
  snapshotSerializers: [
    'jest-preset-angular/AngularSnapshotSerializer.js',
    'jest-preset-angular/HTMLCommentSerializer.js'
  ]
};
