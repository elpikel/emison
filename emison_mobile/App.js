// Import the screens
import Main from './components/Main';
import Chat from './components/Chat';
// Import React Navigation
import { createStackNavigator, createAppContainer } from 'react-navigation'

// Create the navigator
const AppNavigator = createStackNavigator({
  Main: { screen: Main },
  Chat: { screen: Chat },
});

// Export it as the root component
export default createAppContainer(AppNavigator);