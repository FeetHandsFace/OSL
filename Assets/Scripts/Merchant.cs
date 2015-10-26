using System;

public class Merchant {

	public int stolenFrom, shortChanged;
	Inventory inven;
	Secret randomSecret;

	public Merchant(Inventory invntry, Secret rando) {
		randomSecret = rando;
		inven = invntry;
		stolenFrom = 0;
		shortChanged = 0;
	}

	public Merchant(Inventory invntry, Secret rando, int stolen, int shortted) {
		randomSecret = rando;
		inven = invntry;
		stolenFrom = stolen;
		shortChanged = shortted;
	}
	
	public void beginTrade() {
		inven.initiateTrade(randomSecret, this);
	}
}
