module.exports = {
  name: 'ngrx-router',
  preset: '../../jest.config.js',
  coverageDirectory: '../../coverage/libs/ngrx-router',
  snapshotSerializers: [
    'jest-preset-angular/AngularSnapshotSerializer.js',
    'jest-preset-angular/HTMLCommentSerializer.js'
  ]
};
