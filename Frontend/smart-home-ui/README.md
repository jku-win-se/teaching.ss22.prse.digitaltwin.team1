# Getting Started

## Installation Guide

This guide assumes that this application will be used on a barebone computer. If you some of the tools are already installed, skip the already completed steps and proceed with the guide.

### Install Node.js + npm

To use this application you first need to install the Javascript Runtime **Node.js** which comes bundled with a package manager **npm** for managing the dependencies of your application.

#### Windows

For Windows User just download the LTS version here and execute it: [Node.js](https://nodejs.org/en/download/)

to test if the installation was successful open a **cmd** and type

```bash
node -v & npm --v
```

else restart your computer and try again.

#### Linux

For Linux users please visit this guide: [Installing Node.js and npm from NodeSource](https://linuxize.com/post/how-to-install-node-js-on-ubuntu-20-04/#installing-nodejs-and-npm-from-nodesource)

### Install Git

Git is used for source control. It is needed for cloning the repository.

#### Windows

As a Windows user just download and execute the Standalone Installer here: [Download Git](http://git-scm.com/download/win)

as with Node you can test if the installation was successful with following command

```bash
git --version
```

#### Linux

If you are a Linux user use the command

```bash
sudo apt install git-all
```

### Clone the Project

After installing all the necessary prerequisites you can now clone the Project.

In your file system navigate to a folder where you want this project to be stored and open a **cmd**. Now type in the following command:

```bash
git clone https://github.com/jku-win-se/teaching.ss22.prse.digitaltwin.team1.git
```

You are now able to see the different contents of this repository on your computer.

### Installing Dependencies

After cloning the project open a **cmd** and navigate to `teaching.ss22.prse.digitaltwin.team1\Frontend\smart-home-ui` and type:

```bash
npm install
```

This command installs all dependencies you need for executing the application.

> After finishing all the steps you are now able to continue to the **Available Scripts** section and use the application as you wish.

## Available Scripts

In the project directory, you can run:

### `npm start`

Runs the app in the development mode.\
Open [http://localhost:3000](http://localhost:3000) to view it in the browser.

The page will reload if you make edits.\
You will also see any lint errors in the console.

### `npm test`

Launches the test runner in the interactive watch mode.\
See the section about [running tests](https://facebook.github.io/create-react-app/docs/running-tests) for more information.

### `npm run build`

Builds the app for production to the `build` folder.\
It correctly bundles React in production mode and optimizes the build for the best performance.

The build is minified and the filenames include the hashes.\
Your app is ready to be deployed!

See the section about [deployment](https://facebook.github.io/create-react-app/docs/deployment) for more information.

### `npm run eject`

**Note: this is a one-way operation. Once you `eject`, you can’t go back!**

If you aren’t satisfied with the build tool and configuration choices, you can `eject` at any time. This command will remove the single build dependency from your project.

Instead, it will copy all the configuration files and the transitive dependencies (webpack, Babel, ESLint, etc) right into your project so you have full control over them. All of the commands except `eject` will still work, but they will point to the copied scripts so you can tweak them. At this point you’re on your own.

You don’t have to ever use `eject`. The curated feature set is suitable for small and middle deployments, and you shouldn’t feel obligated to use this feature. However we understand that this tool wouldn’t be useful if you couldn’t customize it when you are ready for it.

## Learn More

You can learn more in the [Create React App documentation](https://facebook.github.io/create-react-app/docs/getting-started).

To learn React, check out the [React documentation](https://reactjs.org/).
