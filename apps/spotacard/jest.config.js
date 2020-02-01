module.exports = {
  name: 'spotacard',
  preset: '../../jest.config.js',
  coverageDirectory: '../../coverage/apps/spotacard',
  snapshotSerializers: [
    'jest-preset-angular/AngularSnapshotSerializer.js',
    'jest-preset-angular/HTMLCommentSerializer.js'
  ]
};
