using System;
using System.Collections;

namespace BajuGW
{
    class Screen
    {
        void showScreen() {
		
		}
		
		void hideScreen() {
		
		}
		
		void showNotification() {
		
		}
		
		void showError() {
		
		}
    }

    class WardrobeScreen : Screen
    {
		private void refresh() {
		
		}
		
		void showClothDetails(Cloth cloth) {
		
		}
		void showSuggestion(ArrayList<Cloth> clothes) {
		
		}
		
		void showCategories(ArrayList<String> categories) {
		
		}
		
		void showClothes(ArrayList<Cloth> clothes) {
		
		}
		
		void showDeleteConfirmation() {
		
		}
    }

    class FittingScreen : Screen
    { 
		void updateProjection(Tuple<int, int>) {
		
		}
    }

    class OnlineStoreScreen : Screen
    { 
		private void refresh() {
		
		}
		
		void showClothDetails(Cloth cloth) {
		
		}
		
		void showClothes(ArrayList<Cloth> clothes) {
		
		}
		
		void showPurchaseConfirmation() {
		
		}
    }

    class LoginScreen : Screen
    {

    }

    class MainScreen : Screen
    {
		void showSaveConfirmation() {
		
		}
    }

    class RegistrationScreen : Screen
    {

    }

    class ConfigurationScreen : Screen
    {

    }
        
    class BodyScanScreen : Screen
    {
		void updateProjection(Tuple<int, int>) {
		
		}
    }

    class ConnectScreen : Screen
    {

    }

    class ClothScanScreen : Screen
    {

    }

    class AlternativeLoginScreen : Screen
    {

    }

    class MixAndMatchScreen : Screen
    {

    }

    class CategoryScreen : Screen
    {
		void showCategories(ArrayList<String> categories) {
		
		}
		
		void showNewCategoryScreen() {
		
		}
		
		void showDeleteConfirmation() {
		
		}
		
		void showEditCategoryScreen() {
		
		}
    }
}
