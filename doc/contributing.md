# CONTRIBUTING: codebase-repository-template

When contributing to this repository, please first discuss the change you wish to make via issue, email, or any other method with the owners of this repository before making a change.

Please note we have a [Code of Conduct](code-of-conduct.md), please follow it in all your interactions with the project.

## CODE ORGANIZATION
This project's file and directory structure is as follows:
```
Repository/
├── .gitattributes
├── .gitignore
├── bin/
├── build/
│   ├── debug/
│   └── release/
├── doc/
│   ├── development/
│   │   ├── changelog.md
│   │   ├── snippits.txt
│   │   ├── development-notes.md
│   │   ├── known-issues.md
│   │   ├── roadmap.md
│   │   └── scratchpad.txt
│   ├── manual/
│   │   └─ img/
│   ├── howto/
│   │   └─ img/
│   └── repository/
│       ├── acknowledgements.md
│       ├── additional-reading.md
│       ├── code-of-conduct.md
│       ├── contributing.md
│       ├── contributors.md
│       ├── issue-template.md
│       ├── notices.md
│       ├── pull-request-template.md
│       ├── support.md
│       └── testing.md
├── release
│   ├── hotfix
│   ├── patch
│   ├── stable
│   └── update
├── resource
│   └── img/
├── src/
│   ├── ProjData/
│   │   ├── assets/
│   │   │   ├── font/
│   │   │   ├── image/
│   │   │   ├── sound/
│   │   │   └── video/
│   │   ├── common/
│   │   │   └── *.*
│   │   ├── configuration/
│   │   │   └── *.*
│   │   ├── data/
│   │   │   ├── cache/
│   │   │   ├── database/
│   │   │   ├── export/
│   │   │   ├── import/
│   │   │   ├── input/
│   │   │   ├── output/
│   │   │   ├── runtime/
│   │   │   └── tmp/
│   │   ├── extention/
│   │   │   └── *.*
│   │   ├── doc/
│   │   │   └── *.*
│   │   ├── help/
│   │   │   └── *.*
│   │   ├── misc/
│   │   │   └── *.*
│   │   ├── script/
│   │   │   ├── launch/
│   │   │   ├── runalways/
│   │   │   ├── runonce/
│   │   │   └── exit/
│   │   └── template/
│   │       └── *.*
│   └── *.*
├── LICENSE.md
└── README.md
```

`.gitattributes`
Specifies intentionally untracked files to ignore.

`.gitignore`
Defining attributes per path.

`bin/`
All executables go here. This folder is not committed to the repository.

`build/`
Build resources. This folder is not committed to the repository.

`doc/`
Documentation, including repository documentation and application manuals.

`release`
Application releases, including patches and updates.

`resource`
Repository resources, such as images.

`src/`
Sourcecode goes here.

`LICENSE.md`
Repository license.

`README.md`
Repository README.